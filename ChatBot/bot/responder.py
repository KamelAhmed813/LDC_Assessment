import os
from transformers import pipeline

def generate_response(query, context):
    model_name="EleutherAI/gpt-neo-1.3B"
    generator = pipeline('text-generation', model=model_name)
    prompt = f"Context: {context}\n\nQuestion: {query}\n\nAnswer:"
    response = generator(prompt, eos_token_id=50256, do_sample=True, temperature=0.1, min_length=50, max_length=150, truncation=True, top_k=50, repetition_penalty=2.0)
    return response[0]['generated_text'].replace(prompt, "").strip().split('\n')[0]