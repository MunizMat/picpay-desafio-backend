package transfers

import (
	"net/http"

	user "github.com/MunizMat/picpay-desafio-backend/api/internal/modules/users"
	"github.com/gin-gonic/gin"
	"github.com/gin-gonic/gin/binding"
)

type PostInput struct {
	Value float64 `json:"value" binding:"required"`
	Payer int     `json:"payer" binding:"required"`
	Payee int     `json:"payee" binding:"required"`
}

func Post(context *gin.Context) {
	var body PostInput

	err := context.ShouldBindWith(&body, binding.JSON)

	if err != nil {
		context.AbortWithError(http.StatusBadRequest, err)
		return
	}

	userId, _ := context.Get("userId")

	payerWallet := user.GetWallet(int(userId.(int)))

	err = ValidateTransfer(payerWallet, body.Value)

	if err != nil {
		context.AbortWithError(http.StatusBadRequest, err)
		return
	}

	context.JSON(http.StatusOK, gin.H{"message": "Success"})
}
