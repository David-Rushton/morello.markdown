#!/bin/bash

echo $( grep '<Version>[0-9].[0-9].[0-9]<\/Version>' ./Morello.MarkdownCli/Markdown.Cli.csproj | grep '[0-9].[0-9].[0-9]' --only-matching )
