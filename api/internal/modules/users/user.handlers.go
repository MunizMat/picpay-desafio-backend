package user

import (
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/gin-gonic/gin/binding"
)

func Post(context *gin.Context) {
	var user UserModel

	err := context.ShouldBindWith(&user, binding.JSON)

	if err != nil {
		context.AbortWithError(http.StatusBadRequest, err)
		return
	}

	err = CreateUser(&user)

	if err != nil {
		context.AbortWithError(http.StatusBadRequest, err)
		return
	}

	context.JSON(http.StatusOK, gin.H{"message": "User created successfully!"})
}
