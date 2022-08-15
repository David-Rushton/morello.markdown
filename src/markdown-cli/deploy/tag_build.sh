#!/bin/bash

version=$( ./deploy/get_version.sh )
tag="$( ./deploy/get_tag.sh )"

git tag -a markdown-cli-latest --force
git tag -a $tag -m "Release $tag"
