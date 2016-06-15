#Vagrantforhomework
ip is 55.55.55.5
website link is mydem1.com

if (error for mysql){
    run those command;
    sudo apt-get purge mysql-client-core-5.6
    sudo apt-get autoremove
    sudo apt-get autoclean
    sudo apt-get install mysql-client-core-5.5
    sudo apt-get install mysql-server
    exit;
    vagrant reload --provision
}
