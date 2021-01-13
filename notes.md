Deploy terraform

    /env folder

Add User

    &"C:\Program Files\RabbitMQ Server\rabbitmq_server-3.8.9\sbin\rabbitmqctl.bat" add_user 'demo' '<somepassword>' -n rabbit@rmq-rabbit.uksouth.azurecontainer.io --longnames

Set Permissions to Everything!

    &"C:\Program Files\RabbitMQ Server\rabbitmq_server-3.8.9\sbin\rabbitmqctl.bat" set_permissions 'demo' ".*" ".*" ".*" -n rabbit@rmq-rabbit.uksouth.azurecontainer.io --longnames

commands for demo apps 

RabbitMQSendConsole

    dotnet run <host> <queue> <user> <password> <message>
    e.g. dotnet run "rmq-rabbit.uksouth.azurecontainer.io" "demo" "demo" "<somepassword>" "BOOM!"

RabbitMQReceiveConsole

    dotnet run <host> <queue> <user> <password>
    e.g. dotnet run "rmq-rabbit.uksouth.azurecontainer.io" "demo" "demo" "<somepassword>"

Workflow Function app has function which is triggered on the demo queue (configured in app settings)

    e.g. "RabbitMQConnectionAppSetting": "amqp://demo:<somepassword>@rmq-rabbit.uksouth.azurecontainer.io"