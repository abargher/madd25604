#!/usr/bin/env bash

FIELD_DIR="field-new"

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

./$FIELD_DIR/field.sh \
    -workspace "$HOME/CMSC/madd25604/sketch$NUM/workspace" \
    -file "sketch$NUM.field2"
