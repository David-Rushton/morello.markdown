#!/bin/bash

version=$( ./deploy/get_version.sh )
tag="$( ./deploy/get_tag.sh )-dev"

git tag -a markdown-console-latest --force
git tag -a $tag -m "Release $tag"
