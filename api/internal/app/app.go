package app

import (
	"github.com/MunizMat/picpay-desafio-backend/api/internal/clients"
	"github.com/MunizMat/picpay-desafio-backend/api/internal/configs"
	user "github.com/MunizMat/picpay-desafio-backend/api/internal/modules/users"
	"github.com/gin-gonic/gin"
)

func Run() {
	configs.ParseEnvsOrPanic()

	clients.Init()

	app := gin.Default()

	app.Use(CorsMiddleware())
	app.Use(ErrorHandler)

	userRouter := app.Group("/users")

	userRouter.POST("/", user.Post)

	app.Run(":3000")
}
