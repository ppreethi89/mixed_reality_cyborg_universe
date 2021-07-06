import nltk
from nltk.stem.porter import *
from nltk.stem import WordNetLemmatizer, SnowballStemmer
from gensim.test.utils import datapath
from gensim.parsing.preprocessing import STOPWORDS
from gensim.utils import simple_preprocess
from gensim import corpora, models
from flask import jsonify
import gensim
import pickle
from nltk.corpus import wordnet
from pprint import pprint
nltk.download('wordnet')

wordnet.ensure_loaded()


class ImageCrawler:
    def __init__(self, theme, image_index_location="image_index.pkl", topic_model_location="lda_model.pkl", dictionary_location="dictionary.pkl"):
        # Jerry's theme matching code goes here
        self.theme = theme
        self.__image_index_location = image_index_location
        self.__topic_model_location = topic_model_location
        self.__dictionary_location = dictionary_location
        self.__topic_model = self.__load_topic_model()
        self.__image_index = self.__load_image_index()
        self.__dictionary = self.__load_dictionary()
        pass

    # Load image_index, topic model, dictionary
    def __load_topic_model(self):
        location = self.__topic_model_location
        with open(location, "rb") as file:
            topic_model = pickle.load(file)
        return topic_model

    def __load_image_index(self):
        location = self.__image_index_location
        with open(location, "rb") as file:
            image_index = pickle.load(file)
        return image_index

    def __load_dictionary(self):
        location = self.__dictionary_location
        with open(location, "rb") as file:
            dictionary = pickle.load(file)
        return dictionary

    def __synonyms(self, word):
        list = ""
        synonyms_found = []
        for syn in wordnet.synsets(word):
            for name in syn.lemma_names():
                if name not in synonyms_found:
                    list = list + " " + name.replace("_", " ")
                synonyms_found.append(name)
        return list

    def __add_synonyms_to_sentence(self, sentence):
        final = ""

        for word in sentence.split():
            final = final + self.__synonyms(word)
        return final

    def __lemmatize_stemming(self, text):
        return PorterStemmer().stem(WordNetLemmatizer().lemmatize(text, pos='v'))

    def __preprocess(self, text):
        result = []
        for token in gensim.utils.simple_preprocess(text):
            if token not in gensim.parsing.preprocessing.STOPWORDS and len(token) > 3:
                result.append(self.__lemmatize_stemming(token))
        return result

    def matching_image_locations(self, matching_threshold):
        # convert the theme to topics
        relevant_topics = []
        theme = self.theme
        topic_model = self.__topic_model
        image_index = self.__image_index
        dictionary = self.__dictionary

        unseen_doc_with_syn = self.__add_synonyms_to_sentence(theme)
        bow_vector = dictionary.doc2bow(self.__preprocess(unseen_doc_with_syn))
        for index, score in topic_model[bow_vector]:
            relevant_topics.append(index)

        # find images that match those topics
        img_list = []

        for img_entry in image_index:
            total_score = 0
            topic_scores = []

            for topic in img_entry['topics']:
                if topic['topic_index'] in relevant_topics:
                    total_score = total_score + topic['score']
                    topic_score_detail = {
                        'score': topic['score'],
                        'topic_index': topic['topic_index']
                        # 'topic_tags': topic_model.print_topics(-1)[topic['topic_index']][1]
                        # 'topic_tags': topic_model.show_topics()
                    }
                    topic_scores.append(topic_score_detail)

            if total_score > matching_threshold:
                list_entry = {
                    'img': img_entry['img'],
                    'img_tag': img_entry['label'],
                    'total_score': total_score,
                    'topic_scores': topic_scores
                }
                img_list.append(list_entry)

        num_imgs_to_select = 25
        selected_imgs = []
        for entry in sorted(img_list, key=lambda x: x['total_score'], reverse=True)[:num_imgs_to_select]:
            # Getting only the name of the image from the path
            image_name = entry['img'].split('thumbnails/')
            selected_imgs.append(image_name[1])

            # selected_imgs_dict = {i+1: selected_imgs[i] for i in range(0, len(selected_imgs))}

        potential_images = {}
        potential_images['potential_images'] = (selected_imgs)
        return jsonify(potential_images)
