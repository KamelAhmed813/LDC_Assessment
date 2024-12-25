from sentence_transformers import SentenceTransformer
import numpy as np

def retrieve_relevant_chunks(query, vectorStore):
    print("Start retrieve_relevant_chunks")
    model = SentenceTransformer('sentence-transformers/all-MiniLM-L6-v2')
    queryEmbed = model.encode([query])
    print("Query Encoded")
    distances, indices = vectorStore.search(np.array(queryEmbed), 1)
    print("Search done and try to retreve chunk "+str(indices[0][0]))
    relevantChunk = open(rf'E:\project\LDC_Assessment\ChatBot\data\fileChunks\chunk{indices[0][0]}.txt', "r").read()
    print("The most relevant chunk is\n"+relevantChunk)
    #indices index of chunk
    return relevantChunk
    #return [doc.page_content for doc in docs]
