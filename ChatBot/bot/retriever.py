from sentence_transformers import SentenceTransformer
import numpy as np

def retrieve_relevant_chunks(query, vectorStore):
    model = SentenceTransformer('sentence-transformers/all-MiniLM-L6-v2')
    queryEmbed = model.encode([query])
    distances, indices = vectorStore.search(np.array(queryEmbed), 1)
    relevantChunk = open(rf'E:\project\LDC_Assessment\ChatBot\data\fileChunks\chunk{indices[0][0]}.txt', "r").read()
    return relevantChunk
