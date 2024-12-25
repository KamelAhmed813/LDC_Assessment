import faiss
from bot.embeddings import embed_and_index
from bot.retriever import retrieve_relevant_chunks
from bot.responder import generate_response
from bot.fileSplitter import createFileChunks
import os

def prepare_vectorStore():
    chunksDirectory = createFileChunks(r"E:\project\LDC_Assessment\ChatBot\data\documents")
    embed_and_index(chunksDirectory)

def query_pipeline(query):
    directory = r"E:\project\LDC_Assessment\ChatBot\data\fileChunks"
    AllChunks = [open(os.path.join(directory, f), 'r').read() for f in os.listdir(directory)]
    print("Chunks length is "+str(len(AllChunks)))
    vectorStore = faiss.read_index(r'E:\project\LDC_Assessment\ChatBot\data\vectorStore\vector_store.faiss')

    context = retrieve_relevant_chunks(query, vectorStore)
    print("retrieve_relevant_chunks Ended and the context is\n"+context)
    return generate_response(query, context)
