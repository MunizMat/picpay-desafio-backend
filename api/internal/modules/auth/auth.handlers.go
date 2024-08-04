package auth

import (
	"net/http"

	"github.com/gin-gonic/gin"
)

func Get(context *gin.Context) {
	credentials, err := HandleBasicAuth(context.GetHeader("Authorization"))

	if err != nil {
		context.AbortWithError(http.StatusUnauthorized, err)
		return
	}

	token, err := GetToken(credentials)

	if err != nil {
		context.AbortWithError(http.StatusInternalServerError, err)
		return
	}

	context.JSON(http.StatusOK, gin.H{"token": token})

}
