import traceback
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from bot.pipeline import query_pipeline, prepare_vectorStore

prepare_vectorStore()
app = FastAPI()

class Query(BaseModel):
    query: str

@app.post("/ask")
async def ask_bot(query: Query):
    try:
        return query_pipeline(query.query)
    except Exception as e:
        tb = traceback.format_exc()
        raise HTTPException(status_code=500, detail=f"Error processing query in function '{__name__}': {str(e)}")