package constants

type Permission map[string][]string

var commonPermissions Permission = Permission{
	"/transfers/": {"POST"},
	"/auth/":      {"GET"},
	"/users/":     {"POST"},
}

var shopkeeperPermissions Permission = Permission{
	"/auth/":  {"GET"},
	"/users/": {"POST"},
}

var Roles map[string]Permission = map[string]Permission{
	"common":     commonPermissions,
	"shopkeeper": shopkeeperPermissions,
}
