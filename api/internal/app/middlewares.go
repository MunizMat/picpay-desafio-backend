package app

import (
	"errors"
	"net/http"
	"strings"

	"github.com/MunizMat/picpay-desafio-backend/api/internal/configs"
	"github.com/MunizMat/picpay-desafio-backend/api/internal/constants"
	"github.com/gin-contrib/cors"
	"github.com/gin-gonic/gin"
	"github.com/golang-jwt/jwt/v5"
	"github.com/lib/pq"
)

func CorsMiddleware() gin.HandlerFunc {
	return cors.New(cors.Config{
		AllowAllOrigins: true,
		AllowHeaders:    []string{"*"},
	})
}

func ErrorHandler(context *gin.Context) {
	context.Next()

	for _, err := range context.Errors {
		if pgErr, ok := err.Err.(*pq.Error); ok {
			context.JSON(http.StatusBadRequest, gin.H{"error": pgErr.Detail})
			return
		}
	}
}

func PrivateRouteMiddlewate(context *gin.Context) {
	authHeader := context.GetHeader("Authorization")

	if authHeader == "" {
		context.AbortWithError(http.StatusUnauthorized, errors.New("Unauthorized"))
		return
	}

	parts := strings.Split(authHeader, " ")
	token := parts[1]

	if token == "" {
		context.AbortWithError(http.StatusUnauthorized, errors.New("Unauthorized"))
		return
	}

	parser := jwt.NewParser()

	decoded, err := parser.Parse(token, func(t *jwt.Token) (interface{}, error) {
		return []byte(configs.Environment.JWT_SECRET), nil
	})

	if err != nil {
		context.AbortWithError(http.StatusUnauthorized, err)
		return
	}

	claims, ok := decoded.Claims.(jwt.MapClaims)

	if !ok {
		context.AbortWithError(http.StatusUnauthorized, err)
		return
	}

	userType := claims["type"]

	permissions := constants.Roles[userType.(string)]

	path := context.Request.URL.Path

	allowedMethods := permissions[path]

	for _, method := range allowedMethods {
		if method == context.Request.Method {
			context.Set("userId", claims["id"])
			context.Set("userType", userType)
			return
		}
	}

	context.AbortWithError(http.StatusForbidden, errors.New("you are not allowed to perform this operation"))

}
