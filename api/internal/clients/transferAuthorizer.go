package clients

import (
	"encoding/json"
	"fmt"
	"io"
	"net/http"

	"github.com/MunizMat/picpay-desafio-backend/api/internal/configs"
)

type TransferAuthorizerType struct {
	BaseUrl string
}

type AuthorizeOutput struct {
	Status string `json:"status"`
	Data   struct {
		Authorization bool `json:"authorization"`
	} `json:"data"`
}

var (
	TransferAuthorizer *TransferAuthorizerType
)

func CreateTransferAuthorizerClient() {
	TransferAuthorizer = &TransferAuthorizerType{
		BaseUrl: configs.Environment.TRANSFER_AUTHORIZER_URL,
	}
}

func (authorizer *TransferAuthorizerType) Authorize() (*AuthorizeOutput, error) {
	url := fmt.Sprintf("%s/api/v2/authorize", authorizer.BaseUrl)

	response, err := http.Get(url)

	if err != nil {
		return nil, err
	}

	defer response.Body.Close()

	body, err := io.ReadAll(response.Body)

	if err != nil {
		return nil, err
	}

	var output AuthorizeOutput

	err = json.Unmarshal(body, &output)

	if err != nil {
		return nil, err
	}

	return &output, nil
}
