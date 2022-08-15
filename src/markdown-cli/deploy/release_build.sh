#!/bin/bash

version=$( ./deploy/get_version.sh )
tag="$( ./deploy/get_tag.sh )"
release_notes=$(cat ../../docs/markdown-cli/release-notes-v$version.md)

gh create release $tag --title "v$version" --notes-file "$release_notes"
