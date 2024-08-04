package user

import "github.com/MunizMat/picpay-desafio-backend/api/internal/modules/wallets"

func CreateUser(user *UserModel) error {
	hashedPassword, err := HashPassword(user.Password)

	if err != nil {
		return err
	}

	user.Password = hashedPassword

	err = SaveUser(user)

	return err
}

func GetWallet(userId int) *wallets.WalletModel {
	wallet := wallets.FindByUserId(userId)

	return wallet
}
