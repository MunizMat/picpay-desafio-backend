package user

import (
	"github.com/MunizMat/picpay-desafio-backend/api/internal/clients"
)

func SaveUser(user *UserModel) error {
	query := "INSERT INTO users (cpf, email, full_name, password, type) VALUES ($1, $2, $3, $4, $5)"

	_, err := clients.Postgresql.Exec(query, user.Cpf, user.Email, user.FullName, user.Password, user.Type)

	return err
}

func FindByEmail(email string) *UserModel {
	query := "SELECT * FROM users WHERE email = $1"

	row := clients.Postgresql.QueryRow(query, email)

	var user UserModel

	row.Scan(&user.Id, &user.Email, &user.Cpf, &user.FullName, &user.Password, &user.Type)

	return &user
}
