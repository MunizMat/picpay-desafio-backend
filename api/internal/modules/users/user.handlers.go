package user

import (
	"fmt"
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/gin-gonic/gin/binding"
	"github.com/go-playground/validator/v10"
)

func Post(context *gin.Context) {
	var user UserModel

	err := context.ShouldBindWith(&user, binding.JSON)

	if err != nil {
		for _, fieldErr := range err.(validator.ValidationErrors) {
			errorMessage := fmt.Sprintf("Missing required attribute %s", fieldErr.Field())
			context.JSON(http.StatusBadRequest, gin.H{"error": errorMessage})
			return
		}
	}

	err = CreateUser(&user)

	if err != nil {
		fmt.Print(err)
		context.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	context.JSON(http.StatusOK, gin.H{"message": "User created successfully!"})
}
