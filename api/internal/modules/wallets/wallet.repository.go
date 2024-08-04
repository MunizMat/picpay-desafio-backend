package wallets

import "github.com/MunizMat/picpay-desafio-backend/api/internal/clients"

func FindByUserId(userId int) *WalletModel {
	query := "SELECT * FROM wallets WHERE user_id = $1"

	row := clients.Postgresql.QueryRow(query, userId)

	var wallet WalletModel

	row.Scan(&wallet.Id, &wallet.Balance, &wallet.UserId)

	return &wallet
}

func UpdateBalance(walletId int) {
	query := ""
}
