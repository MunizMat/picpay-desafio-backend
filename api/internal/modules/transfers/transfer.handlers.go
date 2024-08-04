package transfers

import (
	"errors"
	"net/http"

	"github.com/gin-gonic/gin"
)

func Post(context *gin.Context) {
	userType, _ := context.Get("userType")

	if userType != "common" {
		context.AbortWithError(http.StatusForbidden, errors.New("you are not allowed to perform this operation"))
		return
	}

	context.JSON(http.StatusOK, gin.H{"message": "Success"})
}
