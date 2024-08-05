package transfers

import (
	"errors"

	"github.com/MunizMat/picpay-desafio-backend/api/internal/clients"
	"github.com/MunizMat/picpay-desafio-backend/api/internal/modules/wallets"
)

func ValidateTransfer(wallet *wallets.WalletModel, transferAmount float64) error {
	if transferAmount > wallet.Balance {
		return errors.New("insufficient balance for transaction")
	}

	result, err := clients.TransferAuthorizer.Authorize()

	if err != nil {
		return err
	}

	if result.Status == "fail" || !result.Data.Authorization {
		return errors.New("failed to authorize transaction")
	}

	return nil
}

func CompleteTransfer(transfer *TransferModel) error {
	err := SaveTransfer(transfer)

	return err
}
