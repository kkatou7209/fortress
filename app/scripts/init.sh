#!/bin/bash

chmod +x /workspace/scripts/*

cd /workspace

dotnet restore 

dotnet tool restore