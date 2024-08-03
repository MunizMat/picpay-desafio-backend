package clients

import (
	"database/sql"
	"log"

	"github.com/MunizMat/picpay-desafio-backend/api/internal/configs"
	_ "github.com/lib/pq"
)

var (
	Postgresql *sql.DB
)

func CreatePostgreSqlClient() {
	db, err := sql.Open("postgres", configs.Environment.PG_CONNECTION_URL)

	if err != nil {
		log.Fatal(err)
	}

	Postgresql = db
}
