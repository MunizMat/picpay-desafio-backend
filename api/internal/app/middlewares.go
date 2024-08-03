package app

import (
	"net/http"

	"github.com/gin-contrib/cors"
	"github.com/gin-gonic/gin"
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
