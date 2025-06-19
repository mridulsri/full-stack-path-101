# Ref:
    - https://www.youtube.com/watch?v=XnYYwcOfcn8&list=PLqAmigZvYxIL9dnYeZEhMoHcoP4zop8-p

# CMD

### setup
- python -m venv env
- .\venv\Scripts\activate
- pip install -r .\requirements.txt
- pip freeze > requirements.txt  [add packages in the file]

### run
- uvicorn main:app   // (<main.py>>:<FastAPI()>)
- uvicorn main:app --port= 
- uvicorn main:app --reload



