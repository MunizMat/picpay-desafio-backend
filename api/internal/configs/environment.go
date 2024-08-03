package configs

import (
	"log"
	"os"

	"github.com/joho/godotenv"
)

type EnvironmentType struct {
	PG_CONNECTION_URL string
}

var (
	Environment *EnvironmentType
)

func ParseEnvsOrPanic() {
	err := godotenv.Load()

	if err != nil {
		log.Panicf(err.Error())
	}

	envVariables := make(map[string]string)

	envVariables["PG_CONNECTION_URL"] = os.Getenv("PG_CONNECTION_URL")

	for varName, value := range envVariables {
		if value == "" {
			log.Panicf("%s is not defined", varName)
		}
	}

	Environment = &EnvironmentType{
		PG_CONNECTION_URL: envVariables["PG_CONNECTION_URL"],
	}
}