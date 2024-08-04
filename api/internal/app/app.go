package app

import (
	"github.com/MunizMat/picpay-desafio-backend/api/internal/clients"
	"github.com/MunizMat/picpay-desafio-backend/api/internal/configs"
	"github.com/MunizMat/picpay-desafio-backend/api/internal/modules/auth"
	"github.com/MunizMat/picpay-desafio-backend/api/internal/modules/transfers"
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
	transfersRouter := app.Group("/transfers", PrivateRouteMiddlewate)

	userRouter.POST("/", user.Post)

	authRouter.GET("/", auth.Get)

	transfersRouter.POST("/", transfers.Post)

	app.Run(":3000")
}
