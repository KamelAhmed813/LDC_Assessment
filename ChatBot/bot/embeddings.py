from sentence_transformers import SentenceTransformer
import numpy as np
import faiss
import os

def embed_and_index(documents):
    texts = [open(f, 'r').read() for f in os.listdir(documents)]

    model = SentenceTransformer('sentence-transformers/all-MiniLM-L6-v2')
    embeddings = model.encode(AllChunks)
    embeddings_array = np.array(embeddings)
    dimension = embeddings_array.shape[1]
    vectorstore = faiss.IndexFlatL2(dimension)
    vectorstore.add(embeddings_array)
    faiss.write_index(vectorstore, r'E:\project\LDC_Assessment\ChatBot\data\vectorStore\vector_store.faiss')

    return vectorstore
