import nltk
from nltk.tokenize import sent_tokenize
import os

nltk.download('punkt')
nltk.download('punkt_tab')

def createFileChunks(documentsDirectory):
    fileChunksDirectory = r"E:\project\LDC_Assessment\ChatBot\data\fileChunks"
    files = [open(os.path.join(documentsDirectory, f), 'r').read() for f in os.listdir(documentsDirectory)]
    fileCount = 1
    for file in files:
        sentences = sent_tokenize(file)
        current_chunk = ""
        for sentence in sentences:
            if len(current_chunk) + len(sentence) <= 500:
                current_chunk += " " + sentence
            else:
                f = open(os.path.join(fileChunksDirectory, f'chunk{fileCount}.txt'), 'w')
                f.write(current_chunk.strip())
                f.close
                fileCount+=1
                current_chunk = sentence
        if current_chunk:  # Add the last chunk
            f = open(os.path.join(fileChunksDirectory, f'chunk{fileCount}.txt'), 'w')
            f.write(current_chunk.strip())
            f.close
            fileCount+=1
    return fileChunksDirectory