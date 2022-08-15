#!/bin/bash

echo "morello-markdown-console-v$( grep '<Version>[0-9].[0-9].[0-9]<\/Version>' ./Morello.MarkdownConsole/Morello.MarkdownConsole.csproj | grep '[0-9].[0-9].[0-9]' --only-matching )"
