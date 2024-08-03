package app

import "github.com/gin-gonic/gin"

func Run() {
	router := gin.Default()

	router.GET("/auth")
	router.POST("/auth")
	router.POST("/transfer")
}
