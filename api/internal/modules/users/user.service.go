package user

func CreateUser(user *UserModel) error {
	hashedPassword, err := HashPassword(user.Password)

	if err != nil {
		return err
	}

	user.Password = hashedPassword

	err = SaveUser(user)

	return err
}
