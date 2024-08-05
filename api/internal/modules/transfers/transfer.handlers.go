package transfers

import (
	"net/http"

	user "github.com/MunizMat/picpay-desafio-backend/api/internal/modules/users"
	"github.com/gin-gonic/gin"
	"github.com/gin-gonic/gin/binding"
)

func Post(context *gin.Context) {
	var transfer TransferModel

	err := context.ShouldBindWith(&transfer, binding.JSON)

	if err != nil {
		context.AbortWithError(http.StatusBadRequest, err)
		return
	}

	payerWallet := user.GetWallet(transfer.PayerId)

	err = ValidateTransfer(payerWallet, transfer.Value)

	if err != nil {
		context.AbortWithError(http.StatusBadRequest, err)
		return
	}

	err = CompleteTransfer(&transfer)

	if err != nil {
		context.AbortWithError(http.StatusBadRequest, err)
		return
	}

	context.JSON(http.StatusOK, gin.H{"message": "Success"})
}
