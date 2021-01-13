Add User

&"C:\Program Files\RabbitMQ Server\rabbitmq_server-3.8.9\sbin\rabbitmqctl.bat" add_user 'demo' 'password123' -n rabbit@rmq-rabbit.uksouth.azurecontainer.io --longnames

Set Permissions to Everything!

&"C:\Program Files\RabbitMQ Server\rabbitmq_server-3.8.9\sbin\rabbitmqctl.bat" set_permissions 'demo' ".*" ".*" ".*" -n rabbit@rmq-rabbit.uksouth.azurecontainer.io --longnames

