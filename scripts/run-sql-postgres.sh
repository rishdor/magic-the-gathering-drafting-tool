cp $1 /tmp/$1
sudo su -l postgres -c "psql -f '/tmp/$1'"
