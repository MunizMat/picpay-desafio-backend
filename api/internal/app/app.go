package app

import (
	"github.com/MunizMat/picpay-desafio-backend/api/internal/clients"
	"github.com/MunizMat/picpay-desafio-backend/api/internal/configs"
	"github.com/MunizMat/picpay-desafio-backend/api/internal/modules/auth"
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
	authRouter := app.Group("/auth")

	userRouter.POST("/", user.Post)

	authRouter.GET("/", auth.Get)

	app.Run(":3000")
}
