OS := $(shell uname)

all:
	dotnet publish  -c Release --output ./build
