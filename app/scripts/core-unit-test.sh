#!/bin/bash

cd /workspace

dotnet test --logger:"console;verbosity=detailed" --filter "Category=Unit"