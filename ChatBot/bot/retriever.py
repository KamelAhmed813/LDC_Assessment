from sentence_transformers import SentenceTransformer
import numpy as np

def retrieve_relevant_chunks(query, vectorStore):
    model = SentenceTransformer('sentence-transformers/all-MiniLM-L6-v2')
    queryEmbed = model.encode([query])
    distances, indices = vectorStore.search(np.array(query), 1)
    #indices index of chunk
    return open(rf'E:\project\LDC_Assessment\ChatBot\data\documents\FileChunks\chunk{indices}.txt', "r").read()
    #return [doc.page_content for doc in docs]
