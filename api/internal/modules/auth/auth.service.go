package auth

import (
	"errors"
	"time"

	user "github.com/MunizMat/picpay-desafio-backend/api/internal/modules/users"
	"github.com/golang-jwt/jwt/v5"
	"golang.org/x/crypto/bcrypt"
)

func GetToken(credentials Credentials) (string, error) {
	user := user.FindByEmail(credentials.Username)

	if user.Email == "" {
		return "", errors.New("invalid credentials")
	}

	err := bcrypt.CompareHashAndPassword([]byte(user.Password), []byte(credentials.Password))

	if err != nil {
		return "", errors.New("wrong password")
	}

	claims := jwt.MapClaims{
		"full_name": user.FullName,
		"userId":    user.Id,
		"email":     user.Email,
		"type":      user.Type,
		"exp":       time.Now().Add(time.Hour * 24).Unix(),
	}

	token, err := CreateToken(claims)

	if err != nil {
		return "", err
	}

	return token, nil
}
