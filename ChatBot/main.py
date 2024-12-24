from wsgiref.validate import validator
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from bot.pipeline import query_pipeline

app = FastAPI()

class RequestModel(BaseModel):
    query: str
    queryId: int

class ResponseModel(BaseModel):
    response: str
    queryId: int

@app.post("/ask", response_model=ResponseModel)
async def ask_bot(request: RequestModel):
    query = request.query
    query_id = request.queryId
    try:
        response = query_pipeline(query)
        return {"queryId": query_id, "response": response}
    except Exception as e:
        # Handle errors and return a 500 status code
        raise HTTPException(status_code=500, detail=f"Error processing query: {str(e)}")