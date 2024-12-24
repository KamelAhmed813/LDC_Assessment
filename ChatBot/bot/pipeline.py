import faiss
from bot.embeddings import embed_and_index
from bot.retriever import retrieve_relevant_chunks
from bot.responder import generate_response
from bot.fileSplitter import createFileChunks
import os

def query_pipeline(query):
    directory = r'E:\project\LDC_Assessment\ChatBot\data\documents\FileChunks'
    AllChunks = [open(os.path.join(directory, f), 'r').read() for f in os.listdir(directory)]
    if(len(AllChunks)<0):
        chunksDirectory = createFileChunks(r"E:\project\LDC_Assessment\ChatBot\data\documents")
        vectorStore = embed_and_index(chunksDirectory)
    else:
        vectorStore = faiss.read_index(r'E:\project\LDC_Assessment\ChatBot\data\vectorStore\vector_store.faiss')

    context = retrieve_relevant_chunks(query, vectorStore)
    return generate_response(query, context)
