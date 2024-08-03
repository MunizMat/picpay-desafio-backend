package user

import (
	"fmt"

	"github.com/MunizMat/picpay-desafio-backend/api/internal/clients"
)

func SaveUser(user *UserModel) error {
	query := "INSERT INTO users (cpf, email, full_name, password) VALUES ($1, $2, $3, $4)"

	_, err := clients.Postgresql.Exec(query, user.Cpf, user.Email, user.FullName, user.Password)

	if err != nil {
		fmt.Print("Error at user.repository.go: ", err)
	}

	return err
}
