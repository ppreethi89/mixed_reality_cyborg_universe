
from flask import Flask
from flask import request
from flask_mail import Mail
from flask_mail import Message
import os


app = Flask(__name__)

#confiure email server and network credentials

app.config['DEBUG'] = True
app.config['MAIL_SERVER'] = 'smtp.office365.com'
app.config['MAIL_PORT'] = 587
app.config['MAIL_USERNAME'] = os.getenv('EMAIL_USERNAME')
app.config['MAIL_PASSWORD'] = os.getenv('EMAIL_PASSWORD')

app.config['MAIL_USE_TLS'] = True
app.config['MAIL_USE_SSL'] = False


mail = Mail(app)



# Make the WSGI interface available at the top level so wfastcgi can get it.
wsgi_app = app.wsgi_app


@app.route('/')
def home():
    
    return "This is home page"


@app.route('/send_email', methods=['GET','POST'])
def send_email():

    mail_From = "cyborg.psm@outlook.com"

    mail_To = "cyborg.psm@outlook.com"

    Subject = "Test Email for Medic";

    Body = "Testing email for Medic";

    message = Message(Subject, sender=mail_From, recipients=[mail_To])
    message.body = Body
    mail.send(message)

    return "Email triggered successfully!"

if __name__ == '__main__':
    
    #HOST = os.environ.get('SERVER_HOST', 'localhost')
    #try:
        #PORT = int(os.environ.get('SERVER_PORT', '5555'))
    #except ValueError:
       # PORT = 5555
    app.run(debug = True)
