package auth

import (
	"encoding/base64"
	"fmt"
	"strings"

	"github.com/MunizMat/picpay-desafio-backend/api/internal/configs"
	"github.com/golang-jwt/jwt/v5"
)

type Credentials struct {
	Username string
	Password string
}

func HandleBasicAuth(authHeader string) (Credentials, error) {
	credentials := Credentials{}

	parts := strings.Split(authHeader, " ")
	base64Credentials := parts[1]

	decoded, err := base64.StdEncoding.DecodeString(base64Credentials)

	if err != nil {
		fmt.Println(err.Error())
		return credentials, err
	}

	decodedString := string(decoded)
	parts = strings.Split(decodedString, ":")

	credentials.Username = parts[0]
	credentials.Password = parts[1]

	return credentials, nil
}

func CreateToken(claims jwt.MapClaims) (string, error) {
	token := jwt.NewWithClaims(jwt.SigningMethodHS256, claims)

	tokenString, err := token.SignedString([]byte(configs.Environment.JWT_SECRET))

	if err != nil {
		return "", err
	}

	fmt.Println(tokenString)
	return tokenString, nil
}
