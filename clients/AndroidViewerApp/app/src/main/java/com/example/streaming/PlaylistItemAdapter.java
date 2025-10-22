package com.example.streaming;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.streaming.models.PlaylistItem;

import java.util.List;

public class PlaylistItemAdapter extends RecyclerView.Adapter<PlaylistItemAdapter.PlaylistItemViewHolder> {
    private List<PlaylistItem> data;

    public PlaylistItemAdapter(List<PlaylistItem> data) {
        this.data = data;
    }

    public void updateData(List<PlaylistItem> newData) {
        data = newData;
        notifyDataSetChanged();
    }

    @NonNull
    @Override
    public PlaylistItemViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_playlist_content, parent, false);
        return new PlaylistItemViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull PlaylistItemViewHolder holder, int position) {
        PlaylistItem item = data.get(position);
        holder.title.setText(item.getOrder() + ". " + item.getTitle());
        holder.description.setText(item.getDescription());
    }

    @Override
    public int getItemCount() {
        return data.size();
    }

    static class PlaylistItemViewHolder extends RecyclerView.ViewHolder {
        final TextView title;
        final TextView description;

        PlaylistItemViewHolder(@NonNull View itemView) {
            super(itemView);
            title = itemView.findViewById(R.id.itemTitle);
            description = itemView.findViewById(R.id.itemDescription);
        }
    }
}
