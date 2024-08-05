package transfers

type TransferModel struct {
	Id      int     `json:"id"`
	Value   float64 `json:"value" binding:"required"`
	PayeeId int     `json:"payeeId" binding:"required"`
	PayerId int     `json:"payerId" binding:"required"`
}
