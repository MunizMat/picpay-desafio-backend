package user

import (
	"github.com/MunizMat/picpay-desafio-backend/api/internal/clients"
)

func SaveUser(user *UserModel) error {
	transaction, err := clients.Postgresql.Begin()

	if err != nil {
		return err
	}

	query := "INSERT INTO users (cpf, email, full_name, password, type) VALUES ($1, $2, $3, $4, $5) RETURNING id"

	err = transaction.QueryRow(query, user.Cpf, user.Email, user.FullName, user.Password, user.Type).Scan(&user.Id)

	if err != nil {
		transaction.Rollback()
		return err
	}

	query = "INSERT INTO wallets (balance, user_id) VALUES ($1, $2)"

	_, err = transaction.Exec(query, 500, user.Id)

	if err != nil {
		transaction.Rollback()
		return err
	}

	err = transaction.Commit()

	return err

}

func FindByEmail(email string) *UserModel {
	query := "SELECT * FROM users WHERE email = $1"

	row := clients.Postgresql.QueryRow(query, email)

	var user UserModel

	row.Scan(&user.Id, &user.Email, &user.Cpf, &user.FullName, &user.Password, &user.Type)

	return &user
}
