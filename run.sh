#!/bin/bash

rm out*.txt
rm nice*.txt

filepath=words.txt
threads=16

# words=( 1000 5000 10000 50000 100000 500000 1000000 5000000 10000000)
words=( 10000000 )
# words=( 50000 )
for number_of_words in "${words[@]}"
do
    output="out$number_of_words.txt"
    dotnet run $filepath $output $number_of_words $threads
done
