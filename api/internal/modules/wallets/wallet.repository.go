package wallets

import "github.com/MunizMat/picpay-desafio-backend/api/internal/clients"

func FindByUserId(userId int) *WalletModel {
	var wallet WalletModel
	query := "SELECT * FROM wallets WHERE user_id = $1"

	clients.Postgresql.QueryRow(query, userId).Scan(&wallet.Id, &wallet.Balance, &wallet.UserId)

	return &wallet
}
