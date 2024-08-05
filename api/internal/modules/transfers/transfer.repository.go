package transfers

import "github.com/MunizMat/picpay-desafio-backend/api/internal/clients"

func SaveTransfer(transfer *TransferModel) error {
	transaction, err := clients.Postgresql.Begin()

	if err != nil {
		return err
	}

	query := "INSERT INTO transfers (value, payer_id, payee_id) VALUES ($1, $2, $3)"

	_, err = transaction.Exec(query, transfer.Value, transfer.PayerId, transfer.PayeeId)

	if err != nil {
		transaction.Rollback()
		return err
	}

	query = "UPDATE wallets SET balance = balance + $1 WHERE user_id = $2"
	_, err = transaction.Exec(query, transfer.Value, transfer.PayeeId)

	if err != nil {
		transaction.Rollback()
		return err
	}

	query = "UPDATE wallets SET balance = balance + $1 WHERE user_id = $2"
	_, err = transaction.Exec(query, -transfer.Value, transfer.PayerId)

	if err != nil {
		transaction.Rollback()
		return err
	}

	err = transaction.Commit()

	return err
}
