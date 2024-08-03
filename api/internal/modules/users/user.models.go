package user

type UserModel struct {
	Id       int    `json:"id"`
	FullName string `json:"full_name" binding:"required"`
	Cpf      string `json:"cpf" binding:"required"`
	Email    string `json:"email" binding:"required"`
	Password string `json:"password" binding:"required"`
	Type     string `json:"type" binding:"required"`
}
