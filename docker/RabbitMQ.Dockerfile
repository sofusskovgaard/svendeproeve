FROM rabbitmq:3.11-management-alpine

RUN rabbitmq-plugins enable rabbitmq_delayed_message_exchange