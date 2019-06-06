#!/bin/bash

dotnet publish -r linux-arm --self-contained --framework netcoreapp2.2
wait
echo "Build done"

echo "Copying application.."
rsync -ru ./bin/Debug/netcoreapp2.2/linux-arm/publish/* pi@192.168.0.241:/home/pi/TextGenerator/application # copy only changed files to Raspberry 
wait
echo "Copying dictionary.."
rsync -u ./words.txt pi@192.168.0.241:/home/pi/TextGenerator/words.txt
wait
echo "Copying done"

# echo "Giving permission to run.."
# ssh pi@192.168.0.241 << EOF
# chmod +x /home/pi/TextGenerator/application/TextGenerator
# EOF

echo "Done"