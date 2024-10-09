#!/usr/bin/env bash

if [[ "$#" -ne 1 ]]; then
    echo "Please specify a sketch number."
    exit 1
fi

re='^[0-9]$'
if ! [[ $1 =~ $re ]]; then
    echo "$1 is not a valid sketch number."
    exit 1
fi

NUM=$1

./field_alpha43_m1/field.sh \
    -destination "$HOME/CMSC/madd25604/sketch$NUM/workspace" \
    -file "$HOME/CMSC/madd25604/sketch$NUM/workspace/sketch$NUM.field2"
